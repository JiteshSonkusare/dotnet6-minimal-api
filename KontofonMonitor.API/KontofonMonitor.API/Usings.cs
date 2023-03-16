﻿global using NLog;
global using NLog.Web;
global using System.Text;
global using Asp.Versioning;
global using System.Xml.Linq;
global using Newtonsoft.Json;
global using Asp.Versioning.Builder;
global using Microsoft.OpenApi.Models;
global using Asp.Versioning.ApiExplorer;
global using Asp.Versioning.Conventions;
global using KontofonMonitor.API.OpenApi;
global using System.Runtime.Serialization;
global using Microsoft.Extensions.Options;
global using System.Text.Json.Serialization;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using Swashbuckle.AspNetCore.Annotations;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Microsoft.AspNetCore.Mvc.ModelBinding;

global using KontofonMonitor.API.Models;
global using KontofonMonitor.API.Config;
global using KontofonMonitor.API.Extensions;
global using KontofonMonitor.API.Application;
global using KontofonMonitor.API.Endpoints.v1;
global using KontofonMonitor.API.Application.Helpers;
global using KontofonMonitor.API.Application.Exceptions;
global using Wrapper = KontofonMonitor.API.Application.Wrapper;