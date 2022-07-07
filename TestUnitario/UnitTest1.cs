using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Configuration;
using MediatR;
using Aplicacion.Categorias;
using FluentValidation;
using Persistencia;
using System.Collections.Generic;
using Dominio;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using APIPagos.Controllers;

namespace TestUnitario
{
    [TestClass]
    public class UnitTest1
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        public static string RaizURL()
        {
            var URL = "http://localhost:47113/api/";
            return URL;
        }

        //prueba unitaria para validar acción de consulta
        /*[TestMethod]
        public async Task AplicacionConsulta_Test()
        {
            var moq = new Mock<IMediator>();
            //Act
            var controller = new CategoriasController();

            //Arrange
            var rpta = await controller.Get();

            //something like:
            Assert.IsNotNull(rpta);
        }*/

        //prueba unitaria para validacion fluent
        /*[TestMethod]
        public void AplicacionConsultaValidation_Test()
        {
            //Arrange
            var consulta = new ConsultaId.CategoriaUnica();
            consulta.Id_categoria = 1000;

            //Act
            var servicio = new ConsultaId.EjecutaValidacion();
            var servicioRpta = servicio.Validate(consulta);

            //Assert
            Assert.IsFalse(servicioRpta.IsValid);
        }*/
    }
}
