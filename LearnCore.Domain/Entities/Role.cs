using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCore.Domain.Entities
{
    /// <summary>
    /// 角色实体类
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual User CreateUser { get; set; }

        /// <summary>
        /// 用户集合
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// 菜单集合
        /// </summary>
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
