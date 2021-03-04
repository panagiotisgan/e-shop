using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.UI.Extensions
{
    public static class AddEmptyElementInList
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<SelectListItem>  AddEmptyElement(this List<SelectListItem> list, string text = "-",bool selected = true, string value = "")
        {
            list.Insert(0, new SelectListItem { Text = text, Value = value, Selected = selected });
            return list;
        }
    }
}
