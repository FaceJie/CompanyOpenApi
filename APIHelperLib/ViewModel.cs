using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIHelperLib
{
    public class ViewModel
    {
    }
    public class ZTreeVM
    {
        public double id { get; set; }

        public int? pId { get; set; }

        public string name { get; set; }

        public bool @checked
        {
            get;
            set;
        }
        public bool isParent { get; set; }

        public bool open { get; set; }
    }
    public class ModuleVM
    {
        public ModuleVM()
        {
            Enabled = true;
            IsMenu = true;
            LinkUrl = "#";
            Code = 9999;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        public string LinkUrl { get; set; }
        public bool IsMenu { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public string StrIsMenu
        {
            get
            {
                return IsMenu == true ? "是" : "否";
            }
        }

        public string StrEnabled
        {
            get
            {
                return Enabled == true ? "是" : "否";
            }
        }
        public DateTime UpdateDate { get; set; }

        public string StrUpdateDate
        {
            get
            {
                return UpdateDate.ToString();
            }
        }

        public IList<ModuleVM> ChildModules { get; set; }

    }
    public class SideBarMenuVM
    {
        public SideBarMenuVM()
        {
            this.ChildMenuList = new List<SideBarMenuVM>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string LinkUrl { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<SideBarMenuVM> ChildMenuList { get; set; }
    }

    public class LoginVM
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public bool IsRememberLogin { get; set; }
    }
    public class CheckBoxVM
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Discription { get; set; }
        public bool IsChecked { get; set; }
    }

    public class MsgModel
    {
        public bool scu { get; set; }
        public string msg { get; set; }
    }
}
