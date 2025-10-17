using Datlo.Application.Ports.Out;
using Datlo.Application.UseCases;
using Datlo.Domain.Entities;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.UnitTest.UseCases
{
    [TestFixture]
    public class ImportFileUseCaseTest
    {
        ImportFileUseCase _useCase;
        private IProcessFile _processor;
        private IImportRepository _repository;

        private const string CsvContent =
        @"Name,Email,LastPaymentValue
          User_1,user1@example.com,477.27
          User_2,user2@example.com,872.62";

        [SetUp]
        public void Setup()
        {
            _processor = Substitute.For<IProcessFile>();
            _repository = Substitute.For<IImportRepository>();
            _useCase = new ImportFileUseCase(_processor, _repository);
        }

        [Test]
        public async Task ShouldImportUsingStreamAndSaveRepositoryFile()
        {
            //Instancia um mock de input
            var fileBytes = Encoding.UTF8.GetBytes(CsvContent);
            var stream = new MemoryStream(fileBytes);
            var file = Substitute.For<IFormFile>();
            file.OpenReadStream().Returns(stream);

            //Prepara a captura do lambda
            Func<IList<ImportedDataModel>, Task> capturedInsertAction = null;
            _processor.ProcessStreamAsync(Arg.Any<Stream>(), Arg.Do<Func<IList<ImportedDataModel>, Task>>(
                action => capturedInsertAction = action
            )).Returns(Task.CompletedTask);

            //Executa
            await _useCase.ExecuteImportAsync(file);

            //Verifica se os metodos do fluxo foram executados
            await _processor.Received(1).ProcessStreamAsync(stream, Arg.Any<Func<IList<ImportedDataModel>, Task>>());
            file.Received(1).OpenReadStream();

            //Verifica se saveBatch esta sendo executado apenas via lambda
            await _repository.Received(0).SaveBatchAsync(Arg.Any<IList<ImportedDataModel>>());
            Assert.NotNull(capturedInsertAction);
            IList<ImportedDataModel> listToBeSentToRepository = new List<ImportedDataModel>();
            await capturedInsertAction.Invoke(listToBeSentToRepository);
            await _repository.Received(1).SaveBatchAsync(listToBeSentToRepository);
        }
    }
}
