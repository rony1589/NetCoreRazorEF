using School.Core.Entities;
using School.Infrastructure.Data;
using School.Infrastructure.Services;
using NSubstitute;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using School.Web.Pages.Students;

namespace School.Tests.Controllers
{
    [TestClass]
    public  class StudentControllerTests: BasePruebas
    {

        [TestMethod]
        public async Task OnGetAsync()
        {
            //Arrage
            var nameDB = Guid.NewGuid().ToString();

            //Insertamos datos de contexto
            var serviceProvider1 = CreateContext(nameDB);
            var context1 = serviceProvider1.GetRequiredService<SchoolDbContext>();

            context1.Students.Add(new Student() { Id = Guid.NewGuid(), Documento = "123", FullName = "Juan Pérez", Email = "juan@hotmail.com" });
            context1.Students.Add(new Student() { Id = Guid.NewGuid(), Documento = "12345", FullName = "Camilo Lopez", Email = "camilo@hotmail.com" });
            await context1.SaveChangesAsync();

            //Usamos nuevo contexto para probar IndexModel
            var serviceProvider2 = CreateContext(nameDB);
            var contexto2 = serviceProvider2.GetRequiredService<SchoolDbContext>();

            var mapper = ConfigurarAutoMapper();
            var studentService = new StudentService(contexto2);
            var logger = Substitute.For<ILogger<IndexModel>>();

            var pageModel = new IndexModel(studentService, logger, mapper);

            //Act
            var result = await pageModel.OnGetAsync();

            //Assert
            Assert.IsNotNull(pageModel.Students);
            Assert.AreEqual(2, pageModel.Students.Count);
            Assert.AreEqual("Juan Pérez", pageModel.Students.First().FullName);
            Assert.AreEqual("123", pageModel.Students.First().Documento);
            Assert.AreEqual("juan@hotmail.com", pageModel.Students.First().Email);
        }

    }
}
