using NUnit.Framework;
using TestRestAPI3.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Serialization.Json;

namespace TestRestAPI.Controllers.Tests
{
    [TestFixture()]
    public class TodoItemsControllerTests
    {
        private const string BASE_URL = "http://localhost:49945/api/TodoItems";

        [Test()]
        public void GetTodoItemsTest()
        {
            // arrange
            RestClient client = new RestClient(BASE_URL);
            RestRequest request =
                new RestRequest(Method.GET);
            // act
            IRestResponse response = client.Execute(request);
            List<TestRestAPI3.Models.TodoItem> ItemList =
                new JsonDeserializer().
                Deserialize<List<TestRestAPI3.Models.TodoItem>>(response);
            // assert
            Assert.That(
                ItemList.Count,
                Is.GreaterThan(0)
            );

        }

        [Test()]
        public void GetTodoItemTest()
        {
            // arrange
            RestClient client = new RestClient(BASE_URL+ "/1");
            RestRequest request =
                new RestRequest(Method.GET);
            // act
            IRestResponse response = client.Execute(request);
            TestRestAPI3.Models.TodoItem todoItem =
                new JsonDeserializer().
                Deserialize<TestRestAPI3.Models.TodoItem>(response);
            // assert
            Assert.That(
                todoItem.Title,
                Is.EqualTo("walk dog")
            );
        }

        [Test()]
        public void PutTodoItemTest()
        {
            // arrange
            RestClient client = new RestClient(BASE_URL + "/1");
            RestRequest request =
                new RestRequest(Method.PUT);
            
            request.AddHeader("Content-type", "application/json");

            request.AddJsonBody(new TestRestAPI3.Models.TodoItem() { Id = 1, Title = "Feed the fish", Description = "Feed the fish description", ExpiryDateTime = new DateTime(2020, 01, 18, 17, 0, 0), PercentCompleted = 100 });
            // act
            IRestResponse response = client.Execute(request);
           
            // assert
            Assert.That(response.IsSuccessful,
                Is.EqualTo(true)
            );
        }

        [Test()]
        public void PostTodoItemTest()
        {
            // arrange
            RestClient client = new RestClient(BASE_URL);
            RestRequest request =
                new RestRequest(Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(new TestRestAPI3.Models.TodoItem() {Title = "See dentist", Description = "Visit Dentist", ExpiryDateTime = DateTime.Today, PercentCompleted = 10 });
            // act
            IRestResponse response = client.Execute(request);
            TestRestAPI3.Models.TodoItem todoItem =
                new JsonDeserializer().
                Deserialize<TestRestAPI3.Models.TodoItem>(response);
            // assert
            Assert.That(
                todoItem.Title,
                Is.EqualTo("See dentist")
            );
        }

        [Test()]
        public void DeleteTodoItemTest()
        {
            // arrange
            RestClient client = new RestClient(BASE_URL + "/1");
            RestRequest request =
                new RestRequest(Method.DELETE);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(new TestRestAPI3.Models.TodoItem() { Id = 1, Title = "Feed the fish", Description = "Feed the fish description", ExpiryDateTime = DateTime.Today, PercentCompleted = 10 });
            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.That(response.IsSuccessful,Is.EqualTo(true));
        }
    }
}