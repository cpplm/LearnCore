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
        /// 角色编码
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
        /// 角色菜单集合
        /// </summary>
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
