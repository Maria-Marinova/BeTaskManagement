using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using BeTaskManagement.Models.Enums;

namespace BeTaskManagement.Helpers
{
    public class EnumHelper : MarkupExtension
    {
        public Type EnumType { get; private set; }
        public EnumHelper(Type enumType)
        {
            EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
        }
    }
}
