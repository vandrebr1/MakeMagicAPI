using AutoMapper;
using MakeMagic.Controllers;
using MakeMagic.Data;
using MakeMagic.Data.DTOs;
using MakeMagic.HttpClients;
using MakeMagic.Models;
using MakeMagic.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MakeMagic.Tests
{
    public class CharacterControllerTests
    {
        /// <summary>
        /// Test a valid character object
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateAsync_WhenValidCharacter_ReturnCreatedAtActionResult()
        {
            //Arrange
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new CharacterProfile()); });
            var mapper = mockMapper.CreateMapper();

            var httpClient = RetornarHttpClient();

            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("TmpDb").Options;
            var contexto = new DataContext(options);

            var characterController = new CharacterController(contexto, mapper: mapper, httpClient);
            var character = NewCharacter();

            //Act
            var result = await characterController.Create(character);

            //Assert
            Assert.IsType<CreatedAtActionResult>(result);
            var createdCharacter = result as CreatedAtActionResult;
            Assert.IsType<Character>(createdCharacter.Value);
        }

        [Fact]
        public async Task CreateAsync_WhenInvalidHouse_ReturnNotFound()
        {
            //Arrange
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new CharacterProfile()); });
            var mapper = mockMapper.CreateMapper();

            var httpClient = RetornarHttpClient();

            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("TmpDb").Options;
            var contexto = new DataContext(options);

            var characterController = new CharacterController(contexto, mapper: mapper, httpClient);

            var character = NewCharacter();
            character.House = "1760529f-6d51-4cb1-bcb1-ZZZZZZZZZZZZ";

            //Act
            var result = await characterController.Create(character);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task SelectByIdAsync_WhenCharacterExist_ReturnsOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("TmpDb").Options;
            var contexto = new DataContext(options);

            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new CharacterProfile()); });
            var mapper = mockMapper.CreateMapper();

            var httpClient = RetornarHttpClient();
            var characterController = new CharacterController(contexto, mapper: mapper, httpClient);
            var character = NewCharacter();

            //Act
            var result = await characterController.Create(character);
            var createdCharacter = (Character)(result as CreatedAtActionResult).Value;
            var resultSelect = await characterController.SelectById(createdCharacter.Id);

            //Assert
            Assert.IsType<OkObjectResult>(resultSelect.Result);
        }

        [Fact]
        public async Task SelectByIdAsync_WhenCharacterNotExist_ReturnsNotFound()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("TmpDb").Options;
            var contexto = new DataContext(options);

            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new CharacterProfile()); });
            var mapper = mockMapper.CreateMapper();

            var httpClient = RetornarHttpClient();
            var characterController = new CharacterController(contexto, mapper: mapper, httpClient);

            //Act
            var result = await characterController.SelectById(99999);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateAsync_WhenCharacterExist_NoContentResult()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("TmpDb").Options;
            var contexto = new DataContext(options);

            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new CharacterProfile()); });
            var mapper = mockMapper.CreateMapper();

            var httpClient = RetornarHttpClient();
            var characterController = new CharacterController(contexto, mapper: mapper, httpClient);

            var result = await characterController.Create(NewCharacter());
            var createdCharacter = (Character)(result as CreatedAtActionResult).Value;

            //Act
            var characterUpdate = NewCharacter();
            var resultUpdate = await characterController.Update(createdCharacter.Id, characterUpdate);

            //Assert
            Assert.IsType<NoContentResult>(resultUpdate);
        }

        [Fact]
        public async Task UpdateAsync_WhenCharacterNotExist_NotFoundResult()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("TmpDb").Options;
            var contexto = new DataContext(options);

            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new CharacterProfile()); });
            var mapper = mockMapper.CreateMapper();

            var httpClient = RetornarHttpClient();
            var characterController = new CharacterController(contexto, mapper: mapper, httpClient);

            //Act
            var result = await characterController.Update(9999, NewCharacter());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_WhenInvalidHouse_NotFoundResult()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("TmpDb").Options;
            var contexto = new DataContext(options);

            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new CharacterProfile()); });
            var mapper = mockMapper.CreateMapper();

            var httpClient = RetornarHttpClient();
            var characterController = new CharacterController(contexto, mapper: mapper, httpClient);

            var resultCreate = await characterController.Create(NewCharacter());
            var createdCharacter = (Character)(resultCreate as CreatedAtActionResult).Value;

            //Act
            var characterUpdate = NewCharacter();
            characterUpdate.House = "1760529f-6d51-4cb1-bcb1-ZZZZZZZZZZZZ";
            var resultUpdate = await characterController.Update(createdCharacter.Id, characterUpdate);

            //Assert
            Assert.IsType<NotFoundObjectResult>(resultUpdate);
        }

        [Fact]
        public async Task DeleteAsync_WhenValidCharacter_NoContentResult()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("TmpDb").Options;
            var contexto = new DataContext(options);

            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new CharacterProfile()); });
            var mapper = mockMapper.CreateMapper();

            var httpClient = RetornarHttpClient();
            var characterController = new CharacterController(contexto, mapper: mapper, httpClient);

            var resultCreate = await characterController.Create(NewCharacter());
            var createdCharacter = (Character)(resultCreate as CreatedAtActionResult).Value;

            //Act
            var resultDelete = await characterController.Delete(createdCharacter.Id);

            //Assert
            Assert.IsType<NoContentResult>(resultDelete);
        }

        [Fact]
        public async Task DeleteAsync_WhenInvalidCharacter_NoContentResult()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("TmpDb").Options;
            var contexto = new DataContext(options);

            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new CharacterProfile()); });
            var mapper = mockMapper.CreateMapper();

            var httpClient = RetornarHttpClient();
            var characterController = new CharacterController(contexto, mapper: mapper, httpClient);

            //Act
            var resultDelete = await characterController.Delete(99999);

            //Assert
            Assert.IsType<NotFoundResult>(resultDelete);
        }
        private CharacterDto NewCharacter()
        {
            return new CharacterDto()
            {
                Name = $"Vandré Potter + {DateTime.Now.ToString("yyyyMMddHHmmss")}",
                House = "1760529f-6d51-4cb1-bcb1-25087fce5bde",
                Patronus = "stag",
                Role = "student",
                School = "Hogwarts School of Witchcraft and Wizardry"
            };
        }
        private HouseApiClient RetornarHttpClient()
        {
            var client = new HttpClient();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true).Build();

            client.BaseAddress = new Uri(config["ApiConfig:BaseUrl"]);
            client.DefaultRequestHeaders.Add("apikey", config["ApiConfig:Token"]);

            ApiConfig apiConfig = new ApiConfig()
            {
                BaseUrl = config["ApiConfig:BaseUrl"],
                Token = config["ApiConfig:Token"],
            };

            return new HouseApiClient(client, apiConfig);
        }
    }
}