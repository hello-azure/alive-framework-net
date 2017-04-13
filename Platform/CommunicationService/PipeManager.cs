/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Alive.Foundation.Data;
using Alive.Foundation.Data.Communication;

namespace LionTH.Myconos
{
    /// <summary>
    /// 消息管道
    /// </summary>
    public sealed class PipeManager
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 消息管道实例
        /// </summary>
        private static readonly PipeManager pipe = new PipeManager();

        /// <summary>
        /// 管道队列
        /// </summary>
        private Dictionary<string, Queue<MessageBase>> pipes = new Dictionary<string, Queue<MessageBase>>();

        /// <summary>
        /// 管道分组信息
        /// </summary>
        private Dictionary<string, string> groups = new Dictionary<string, string>();

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 私有静态构造函数
        /// </summary>
        static PipeManager()
        { 
        
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        PipeManager()
        {

        }

        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 消息管道实例
        /// </summary>
        public static PipeManager Pipe
        {
            get
            {
                return pipe;
            }
        }


        #endregion

        #region ==== 公共方法 ====

        /// <summary>
        /// 添加管道至队列
        /// </summary>
        /// <param name="pipeName">管道名称</param>
        /// <returns>返回一个值，表示管道是否成功创建</returns>
        public bool Create(string pipeName)
        {
            return this.Create(pipeName, string.Empty);
        }

        /// <summary>
        /// 添加管道至队列，并为其指定分组
        /// </summary>
        /// <param name="pipeName">管道名称</param>
        /// <param name="groupName">分组名称</param>
        /// <returns>返回一个值，表示管道是否成功创建</returns>
        public bool Create(string pipeName, string groupName)
        {
            lock (this.pipes)
            {
                if (!pipes.ContainsKey(pipeName))
                {
                    pipes.Add(pipeName, new Queue<MessageBase>());

                    lock (this.groups)
                    {
                        if (!string.IsNullOrEmpty(groupName))
                        {
                            groups.Add(pipeName, groupName);
                        }
                    }

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 注销一个管道
        /// </summary>
        /// <param name="pipeName">管道名称</param>
        public void Remove(string pipeName)
        {
            lock (this.groups)
            {
                if (this.groups.ContainsKey(pipeName))
                {
                    this.groups.Remove(pipeName);
                }
            }

            lock (this.pipes)
            {
                if (this.pipes.ContainsKey(pipeName))
                {
                    this.pipes.Remove(pipeName);
                }
            }
        }

        /// <summary>
        /// 读取指定管道
        /// </summary>
        /// <param name="pipeName">管道名</param>
        /// <returns>返回指定管道的消息列表</returns>
        public Queue<MessageBase> Get(string pipeName)
        {
            lock (this.pipes)
            {
                if (pipes.Count > 0 && pipes.ContainsKey(pipeName))
                {
                    return pipes[pipeName];
                }
            }

            return null;
        }

        /// <summary>
        /// 获得指定名称的管道分组
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <returns>分组内所有管道的名称列表</returns>
        public List<string> GetPipeGroup(string groupName)
        {
            var pipes = from pipe in this.groups
                        where pipe.Value == groupName
                        select pipe.Key;

            lock (this.pipes)
            {
                return pipes.ToList();
            }
        }

        #endregion

    }
}
