using Application.UseCases.Register.Create;
using Application.UseCases.Register.Update;
using Domain.Entities;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Register.Operations
{
    internal class UpdateOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            var register = GetFromRequest(request);
            if (register != null)
            {
                UpdateRegister usecase = DependencyInjection.DI.Get<UpdateRegister>();
                var result = await usecase.Handle(register, cancellationToken);
                var registerResponded = RegisterDto.Of(result.Register);
                if (registerResponded != null)
                {
                    string json = registerResponded.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new EntityNotExistException(this, register);
            }
            throw new UnknownErrorException(this);
        }

        private RegisterEntity? GetFromRequest(HttpControllerRequest request)
        {
            var register = RegisterDto.FromJson(request.Body)?.ToEntity();
            if (register == null)
            {
                var arguments = request.Arguments;
                int id;
                int deviceId;
                string? name;
                string? description;

                name = arguments.Get("Name") ?? throw new QueryInvalidException(this, "Name");
                description = arguments.Get("Description") ?? throw new QueryInvalidException(this, "Description");
                if (!int.TryParse(arguments.Get("DeviceId"), out deviceId)) throw new QueryInvalidException(this, "DeviceId", arguments.Get("DeviceId"));
                if (!!int.TryParse(arguments.Get("Id"), out id)) throw new QueryInvalidException(this, "Id", arguments.Get("Id"));
                
                RegisterEntity entity = new RegisterEntity();
                entity.Id = id;
                entity.DeviceId = deviceId;
                entity.SetName(name);
                entity.SetDescription(description);
                return entity;
            }
            return register;
        }
    }
}
