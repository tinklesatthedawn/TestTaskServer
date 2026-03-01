using Application.UseCases.Device.Delete;
using Application.UseCases.Register.Delete;
using Domain.Entities;
using Network.Dto;
using Network.Http.Exceptions;
using Network.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Register.Operations
{
    internal class DeleteOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            var register = GetFromRequest(request);
            if (register != null)
            {
                DeleteRegister usecase = DependencyInjection.DI.Get<DeleteRegister>();
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

                if (!int.TryParse(arguments.Get("Id"), out id)) throw new QueryInvalidException(this, "Id", arguments.Get("Id"));

                RegisterEntity entity = new RegisterEntity();
                entity.Id = id;

                return entity;
            }
            return register;
        }
    }
}
