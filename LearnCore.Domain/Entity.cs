using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCore.Domain
{
    /// <summary>
    /// 泛型实体基类
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public abstract class Entity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }

    /// <summary>
    /// 定义默认主键为Guid的实体基类
    /// </summary>
    public abstract class Entity : Entity<Guid>
    {

    }
}
