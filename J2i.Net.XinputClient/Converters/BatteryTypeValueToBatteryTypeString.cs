using J2i.Net.XInputWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace J2i.Net.XinputClient.Converters
{
    class BatteryTypeValueToBatteryTypeString:IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte v = (byte)value;
            switch ((BatteryTypes)v)
            {
                case BatteryTypes.BATTERY_TYPE_ALKALINE:
                    return "alkaline";
                case BatteryTypes.BATTERY_TYPE_DISCONNECTED:
                    return "disconnected";
                case BatteryTypes.BATTERY_TYPE_NIMH:
                    return "NiMH";
                case BatteryTypes.BATTERY_TYPE_UNKNOWN:
                    return "unknown";
                case BatteryTypes.BATTERY_TYPE_WIRED:
                    return "wired";
                default:
                    return String.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
