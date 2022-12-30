global using System.Text.Json;
global using System.Reflection;
global using System.Text.Json.Serialization;

global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Mvc;

global using FluentValidation;

global using MediatR;

global using ToDo.Contracts;
global using ToDo.Contracts.ToDos;

global using ToDo.Api.Infrasructure;

global using ToDo.Api.Applications;
global using ToDo.Api.Applications.Todos;
global using ToDo.Api.Applications.Todos.AddNewTodo;
global using ToDo.Api.Applications.Todos.DeleteTodo;
global using ToDo.Api.Applications.Todos.GetTodoById;
global using ToDo.Api.Applications.Todos.ListTodos;
global using ToDo.Api.Applications.Todos.MarkAsDoneTodo;
global using ToDo.Api.Applications.Todos.SearchTodos;
global using ToDo.Api.Applications.Todos.UpdateTodo;

global using ToDo.Api.Presentation;
global using ToDo.Api.Presentation.ViewModels;