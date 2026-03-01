using Application.UseCases.Register.Delete;
using Application.UseCases.RegisterValue.Delete;
using Domain.Entities;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.RegisterValue.Operations
{
    internal class DeleteOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            var register = GetFromRequest(request);
            if (register != null)
            {
                DeleteRegisterValue usecase = DependencyInjection.DI.Get<DeleteRegisterValue>();
                var result = await usecase.Handle(register, cancellationToken);
                var registerValueResponded = RegisterValueDto.Of(result.RegisterValue);
                if (registerValueResponded != null)
                {
                    string json = registerValueResponded.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new EntityNotExistException(this, register);
            }
            throw new UnknownErrorException(this);
        }

        private RegisterValueEntity? GetFromRequest(HttpControllerRequest request)
        {
            var register = RegisterValueDto.FromJson(request.Body)?.ToEntity();
            if (register == null)
            {
                var arguments = request.Arguments;
                int id;

                if (!int.TryParse(arguments.Get("Id"), out id)) throw new QueryInvalidException(this, "Id", arguments.Get("Id"));

                RegisterValueEntity entity = new RegisterValueEntity();
                entity.Id = id;

                return entity;
            }
            return register;
        }
    }
}
