﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;

namespace Core.Aspects.AutoFac
{
    public class LogAspect:MethodInterception
    {
        private LoggerServiceBase _logger;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new Exception(AspectMessages.WrongLoggerType);
            }
            _logger = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _logger.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters=new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter()
                {
                    Name=invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
         
            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
            return logDetail;
        }
    }
}
