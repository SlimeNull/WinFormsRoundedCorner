using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace WinFormsTest
{
    /// <summary>Provides a type converter to convert <see cref="Thickness" /> values to and from various other representations.</summary>
    public class ThicknessConverter : TypeConverter
    {
        /// <summary>Returns whether this converter can convert an object of one type to the type of this converter.</summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from.</param>
        /// <returns>
        ///   <see langword="true" /> if this object can perform the conversion; otherwise, <see langword="false" />.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
        /// <param name="context">The format context.</param>
        /// <param name="destinationType">The type you want to convert to.</param>
        /// <returns>
        ///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
        /// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
        {
            if (value is string text)
            {
                string text2 = text.Trim();
                if (text2.Length == 0)
                {
                    return null;
                }
                if (culture == null)
                {
                    culture = CultureInfo.CurrentCulture;
                }
                char c = culture.TextInfo.ListSeparator[0];
                string[] array = text2.Split(c);
                int[] array2 = new int[array.Length];
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
                for (int i = 0; i < array2.Length; i++)
                {
                    array2[i] = (int)converter.ConvertFromString(context, culture, array[i])!;
                }
                if (array2.Length == 4)
                {
                    return new Thickness(array2[0], array2[1], array2[2], array2[3]);
                }
                throw new ArgumentException();
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
        /// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
        /// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Thickness)
            {
                if (destinationType == typeof(string))
                {
                    Thickness padding = (Thickness)value;
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    string separator = culture.TextInfo.ListSeparator + " ";
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
                    string[] array = new string[4];
                    int num = 0;
                    array[num++] = converter.ConvertToString(context, culture, padding.Left);
                    array[num++] = converter.ConvertToString(context, culture, padding.Top);
                    array[num++] = converter.ConvertToString(context, culture, padding.Right);
                    array[num++] = converter.ConvertToString(context, culture, padding.Bottom);
                    return string.Join(separator, array);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    Thickness padding2 = (Thickness)value;
                    return new InstanceDescriptor(typeof(Thickness).GetConstructor(new Type[4]
                    {
                        typeof(int),
                        typeof(int),
                        typeof(int),
                        typeof(int)
                    }), new object[4] { padding2.Left, padding2.Top, padding2.Right, padding2.Bottom });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values.</param>
        /// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or <see langword="null" /> if the object cannot be created. This method always returns <see langword="null" />.</returns>
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (propertyValues == null)
            {
                throw new ArgumentNullException("propertyValues");
            }
            Thickness padding = (Thickness)context.PropertyDescriptor!.GetValue(context.Instance)!;
            return new Thickness((int)propertyValues["Left"], (int)propertyValues["Top"], (int)propertyValues["Right"], (int)propertyValues["Bottom"]);
        }

        /// <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value, using the specified context.</summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <returns>
        ///   <see langword="true" /> if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, <see langword="false" />.</returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
        /// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
        /// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Thickness), attributes);
            return properties.Sort(new string[5] { "All", "Left", "Top", "Right", "Bottom" });
        }

        /// <summary>Returns whether this object supports properties, using the specified context.</summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <returns>
        ///   <see langword="true" /> if <see cref="Overload:System.ComponentModel.TypeConverter.GetProperties" /> should be called to find the properties of this object; otherwise, <see langword="false" />.</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ThicknessConverter" /> class.</summary>
        public ThicknessConverter()
        {
        }
    }


}
