/***********
 * 版权说明：
 *   本文件是 Somme 服务程序的一部分。
 *   版本：V 1.0
 *   Copyright 北京立安泰华电子科技有限公司 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Lionth.Foundation.Communication
{
    public class MyDuplexChannelFactory<TService>:IDisposable
    {
        private ContractDescription cd = null;
        private ServiceEndpoint endpoint = null;
        private InstanceContext myInstanceContext = null;

        private DuplexChannelFactory<TService> factory = null;

        public MyDuplexChannelFactory(InstanceContext instanceContext, BindingType bindingType, string endpointAddress)
        {
            this.myInstanceContext = instanceContext;
            cd = ContractDescription.GetContract(typeof(TService));
            endpoint = new ServiceEndpoint(cd);

            switch (bindingType)
            {
                case BindingType.NetTcpBinding:
                    NetTcpBindingConfig netTcpBinding = new NetTcpBindingConfig();
                    endpoint.Binding = netTcpBinding.Config;
                    break;
                default:
                    break;
            }

            endpoint.Address = new EndpointAddress(endpointAddress);//@"net.tcp://localhost:8732/NCI/Capetown/BasicService/ServerService/mex");
        }

        public TService CreateChannel()
        {
            factory = new DuplexChannelFactory<TService>(this.myInstanceContext, endpoint);
            return factory.CreateChannel();
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (factory.State != CommunicationState.Closed)
            {
                factory.Close();
            }
        }

        #endregion
    }
}
