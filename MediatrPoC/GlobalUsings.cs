global using System.Text.Json;
global using System.Reflection;
global using System.Text.Json.Serialization;

global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Mvc;

global using FluentValidation;

global using MediatR;

global using MediatrPoC.Presentation.ViewModels;

global using MediatrPoC.Applications;
global using MediatrPoC.Applications.Todos;
global using MediatrPoC.Applications.Todos.AddNewTodo;
global using MediatrPoC.Applications.Todos.UpdateTodo;
global using MediatrPoC.Applications.Todos.MarkAsDoneTodo;
global using MediatrPoC.Applications.Todos.GetTodoById;
global using MediatrPoC.Applications.Todos.ListTodos;
global using MediatrPoC.Applications.Todos.SearchTodos;
global using MediatrPoC.Applications.Todos.DeleteTodo;

global using MediatrPoC.Infrasructure;