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
    public class NetTcpBindingConfig
    {

        private NetTcpBinding netBinding = new NetTcpBinding();



        public NetTcpBindingConfig()
        {
            //<binding name="NetTcpBinding_IServerService" closeTimeout="00:01:00"
            //       openTimeout="00:01:00" receiveTimeout="00:10:00" sendTimeout="00:01:00"
            //       transactionFlow="false" transferMode="Buffered" transactionProtocol="OleTransactions"
            //       hostNameComparisonMode="StrongWildcard" listenBacklog="10"
            //       maxBufferPoolSize="524288" maxBufferSize="65536" maxConnections="10"
            //       maxReceivedMessageSize="65536">
            //       <readerQuotas maxDepth="32" maxStringContentLength="8192" maxArrayLength="16384"
            //           maxBytesPerRead="4096" maxNameTableCharCount="16384" />
            //       <reliableSession ordered="true" inactivityTimeout="00:10:00"
            //           enabled="false" />
            //       <security mode="None">
            //           <transport clientCredentialType="Windows" protectionLevel="EncryptAndSign" />
            //           <message clientCredentialType="Windows" />
            //       </security>

            netBinding.CloseTimeout = new TimeSpan(10, 10, 0);
            netBinding.OpenTimeout = new TimeSpan(10, 10, 0);
            netBinding.ReceiveTimeout = new TimeSpan(10, 10, 0);
            netBinding.SendTimeout = new TimeSpan(10, 10, 0);
            netBinding.TransactionFlow = false;
            netBinding.TransferMode = TransferMode.Buffered;
            netBinding.TransactionProtocol = TransactionProtocol.OleTransactions;
            netBinding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            netBinding.ListenBacklog = 10;
            netBinding.MaxBufferPoolSize = 2147483647;
            netBinding.MaxBufferSize = 2147483647;
            netBinding.MaxConnections = 1000000;
            netBinding.MaxReceivedMessageSize = 2147483647;
 

            netBinding.ReaderQuotas.MaxDepth = 32;
            netBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            netBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            netBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            netBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

            netBinding.ReliableSession.Ordered = true;
            netBinding.ReliableSession.Enabled = false;
            netBinding.ReliableSession.InactivityTimeout = new TimeSpan(10, 10, 0);

            netBinding.Security.Mode = SecurityMode.None;
            netBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            netBinding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            netBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
        }

        public NetTcpBinding Config
        {
            get
            {
                return netBinding;
            }
        }
    }
}
