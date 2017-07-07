using System;
using System.Collections.Generic;
using System.Text;
using LearnCore.Application.MenuApp.Dtos;
using AutoMapper;
using LearnCore.Domain.IRepositories;
using LearnCore.Domain.Entities;
using System.Linq;

namespace LearnCore.Application.MenuApp
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUserRepository _userRepository;
        //private readonly IRoleRepository _roleRepository;
        public MenuAppService(IMenuRepository menuRepository, IUserRepository userRepository)
        {
            _menuRepository = menuRepository;
            _userRepository = userRepository;
            // _roleRepository = roleRepository;
        }

        public bool InsertOrUpdate(MenuDto dto)
        {
            var menu = _menuRepository.InsertOrUpdate(Mapper.Map<Menu>(dto));
            return menu == null ? false : true;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteBatch(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public MenuDto Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<MenuDto> GetAllList()
        {
            var menus = _menuRepository.GetAllList().OrderBy(it => it.SerialNumber);
            //使用AutoMapper进行实体转换
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public List<MenuDto> GetMneusByParent(Guid parentId, int startPage, int pageSize, out int rowCount)
        {
            throw new NotImplementedException();
        }

    }
}
