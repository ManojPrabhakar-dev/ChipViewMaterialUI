using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ChipViewApp.Utils
{
    public enum OPERATING_MODE
    {
        UNKNOWN = 0,
        NORMAL,
        FLOAT,
        DIG_INT
    }

    public enum TIMING_TYPE
    {
        PRE_CONDITION,
        LED,
        MODULATED_STIMULUS,
        INTEG_SEQUENCE,
        NONE
    }

    public enum SAMPLE_TYPE
    {
        STANDARD,
        ONE_REGION_DI_MODE,
        TWO_REGION_DI_MODE,
        IMPULSE_RESPONSE_MODE
    }

    enum SLOT_TYPE
    {
        A = 1,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L
    }

    public enum SELECTED_LED
    {
        LED1,
        LED2,
        LED3,
        LED4
    }

    public enum LED_DRIVESIDE
    {
        LEDA,
        LEDB
    }
    public enum PATH_COMPONENT
    {
        PATH,
        REGULAR_POLYGON,
        ARC
    }

    public enum PAIR_CONFIG
    {
        PAIR12,
        PAIR34,
        PAIR56,
        PAIR78,
        NONE
    }

    enum INPUT_MUX_TYPE
    {
        SINGLE,
        DIFFERENTIAL,
        NONE
    }

    public enum INPUT_PAIR
    {
        INPUT_PAIR_DISABLED = 0,
        INx1_CH1__INx2_DISCONNECTED,
        INx1_CH2__INx2_DISCONNECTED,
        INx1_DISCONNECTED__INx2_CH1,
        INx1_DISCONNECTED__INx2_CH2,
        INx1_CH1__INx2_CH2,
        INx1_CH2__INx2_CH1,
        INx1__INx2_CH1,
        INx1__INx2_CH2
    }

    public static class FrameworkElementExt
    {
        public static void BringToFront(this FrameworkElement element)
        {
            if (element == null) return;

            Canvas parent = element.Parent as Canvas;
            if (parent == null) return;

            var maxZ = parent.Children.OfType<UIElement>()
              .Where(x => x != element)
              .Select(x => Canvas.GetZIndex(x))
              .Max();
            Canvas.SetZIndex(element, maxZ + 1);
        }
    }

    public class aAdpdCtrlRegParams
    {
        public List<Dictionary<string, object>> Parameters = new List<Dictionary<string, object>>();
    }

    public class NAryDictionary<TKey, TValue> :
        Dictionary<TKey, TValue>
    {
    }

    public class NAryDictionary<TKey1, TKey2, TValue> :
        Dictionary<TKey1, NAryDictionary<TKey2, TValue>>
    {
    }
}
