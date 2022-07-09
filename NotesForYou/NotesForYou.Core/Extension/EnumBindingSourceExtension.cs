﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesForYou.Core.Extension
{
    /// <summary>
    /// TODO needed?
    /// </summary>
    [ContentProperty(nameof(Type))]
    public class EnumBindingSourceExtension : IMarkupExtension
    {
        public Type Type { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Type is null || !Type.IsEnum)
                throw new Exception("You must provide a valid enum type");

            return Enum.GetValues(Type);
        }
    }
}
