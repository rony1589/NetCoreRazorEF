using School.Core.Entities;
using School.Infrastructure.Data;
using School.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace School.Tests.Services
{
    [TestClass]
    public class StudentServiceTests : BasePruebas
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public async Task GetAllAsync()
        {
            //Arrange
            var nameDB = Guid.NewGuid().ToString();

            //Crear contexto y agregar datos
            var serviceProvider1 = CreateContext(nameDB);
            var context1 = serviceProvider1.GetRequiredService<SchoolDbContext>();

            context1.Students.Add(new Student() { Id = Guid.NewGuid(), Documento = "123", FullName = "Juan Pérez", Email = "juan@hotmail.com" });
            context1.Students.Add(new Student() { Id = Guid.NewGuid(), Documento = "12345", FullName = "Camilo Lopez", Email = "camilo@hotmail.com" });
            await context1.SaveChangesAsync();

            //Creamos nuevo contexo para el servicio
            var serviceProvider2 = CreateContext(nameDB);
            var contexto2 = serviceProvider2.GetRequiredService<SchoolDbContext>();

            var service = new StudentService(contexto2);

            // Act
            var result = await service.GetAllAsync();
            TestContext.WriteLine($"Mensaje devuelto: {result.ToJson()}");

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(s => s.FullName== "Juan Pérez"));
            Assert.IsTrue(result.Any(s => s.FullName == "Camilo Lopez"));

        }

        [TestMethod]
        public async Task GetByIdAsyncNotExist()
        {
            //Arrange
            var nameDB = Guid.NewGuid().ToString();

            //Crear contexto y agregar datos
            var serviceProvider = CreateContext(nameDB);
            var context = serviceProvider.GetRequiredService<SchoolDbContext>();

            var studentId = Guid.NewGuid();
            var service = new StudentService(context);

            // Act
            var result = await service.GetByIdAsync(studentId);
            TestContext.WriteLine($"Mensaje devuelto: {result.ToJson()}");
            // Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task GetByIdAsyncExist()
        {
            //Arrange
            var nameDB = Guid.NewGuid().ToString();

            //Crear contexto y agregar datos
            var serviceProvider1 = CreateContext(nameDB);
            var context1 = serviceProvider1.GetRequiredService<SchoolDbContext>();

            var studentId = Guid.NewGuid();

            context1.Students.Add(new Student() { Id = studentId, Documento = "123", FullName = "Juan Pérez", Email = "juan@hotmail.com" });
            context1.Students.Add(new Student() { Id = Guid.NewGuid(), Documento = "12345", FullName = "Camilo Lopez", Email = "camilo@hotmail.com" });
            await context1.SaveChangesAsync();

            //Creamos nuevo contexo para el servicio
            var serviceProvider2 = CreateContext(nameDB);
            var contexto2 = serviceProvider2.GetRequiredService<SchoolDbContext>();

            var service = new StudentService(contexto2);

            // Act
            var result = await service.GetByIdAsync(studentId);
            TestContext.WriteLine($"Mensaje devuelto: {result.ToJson()}");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Juan Pérez", result.FullName);
            Assert.AreEqual("123", result.Documento);
        }

        [TestMethod]
        public async Task CreateAsync()
        {
            // Arrange
            var nameDB = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDB);
            var context = serviceProvider.GetRequiredService<SchoolDbContext>();
            var service = new StudentService(context);

            var student = new Student
            {
                Id = Guid.NewGuid(),
                Documento = "6587",
                FullName = "Carlos B",
                Email = "carlos@mail.com"
            };

            // Act
            var result = await service.CreateAsync(student);
            TestContext.WriteLine($"Mensaje devuelto: {result.Message}");

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Estudiante registrado correctamente.", result.Message);

            var saved = await context.Students.FindAsync(student.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual("Carlos B", saved.FullName);

        }

        [TestMethod]
        public async Task CreateAsyncDocumentoAlreadyExistsInAnotherStudent()
        {
            // Arrange
            var nameDB = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDB);
            var context = serviceProvider.GetRequiredService<SchoolDbContext>();
            var service = new StudentService(context);

            var documento = "123";

            context.Students.Add(new Student
            {
                Id = Guid.NewGuid(),
                Documento = documento,
                FullName = "Juan Pérez",
                Email = "juan@hotmail.com"
            });
            await context.SaveChangesAsync();

            var newStudent = new Student
            {
                Id = Guid.NewGuid(),
                Documento = documento,
                FullName = "Raul Cardona",
                Email = "raul@mail.com"
            };

            // Act
            var result = await service.CreateAsync(newStudent);
            TestContext.WriteLine($"Mensaje devuelto: {result.Message}");

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Ya existe un estudiante con ese documento.", result.Message);
        }


        [TestMethod]
        public async Task UpdateAsyncDocumentoAlreadyExistsInAnotherStudent()
        {
            // Arrange
            var nameDB = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDB);
            var context = serviceProvider.GetRequiredService<SchoolDbContext>();
            var service = new StudentService(context);

            var existingStudent = new Student
            {
                Id = Guid.NewGuid(),
                Documento = "777",
                FullName = "Estudiante A",
                Email = "EstudianteA@mail.com"
            };

            var studentToUpdate = new Student
            {
                Id = Guid.NewGuid(),
                Documento = "888",
                FullName = "Estudiante B",
                Email = "EstudianteB@mail.com"
            };

            context.Students.AddRange(existingStudent, studentToUpdate);
            await context.SaveChangesAsync();

            // Intentamos actualizar el segundo con el documento del primero
            studentToUpdate.Documento = "777";

            // Act
            var result = await service.UpdateAsync(studentToUpdate);
            TestContext.WriteLine($"Mensaje devuelto: {result.Message}");

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Otro estudiante ya tiene ese documento.", result.Message);

        }

        [TestMethod]
        public async Task DeleteAsync()
        {
            // Arrange
            var nameDB = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDB);
            var context = serviceProvider.GetRequiredService<SchoolDbContext>();
            var service = new StudentService(context);

            var student = new Student
            {
                Id = Guid.NewGuid(),
                Documento = "1111",
                FullName = "Carlos B",
                Email = "carlos@mail.com"
            };

            context.Students.Add(student);
            await context.SaveChangesAsync();

            // Act
            var result = await service.DeleteAsync(student.Id);
            TestContext.WriteLine($"Mensaje devuelto: {result.Message}");

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Estudiante eliminado correctamente.", result.Message);

            var deleted = await context.Students.FindAsync(student.Id);
            Assert.IsNull(deleted);



        }

        [TestMethod]
        public async Task DeleteAsyncStudentNotFound()
        {
            // Arrange
            var nameDB = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDB);
            var context = serviceProvider.GetRequiredService<SchoolDbContext>();
            var service = new StudentService(context);

            var fakeId = Guid.NewGuid();

            // Act
            var result = await service.DeleteAsync(fakeId);
            TestContext.WriteLine($"Mensaje devuelto: {result.Message?? "Estudiante no encontrado."}");

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Estudiante no encontrado.", result.Message);

        }

    }
}
