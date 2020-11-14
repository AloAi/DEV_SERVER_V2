using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Aloai.Enum;

namespace PartTimeServer.Utility
{
    /// <summary>
    /// Enum Extensions class.
    /// </summary>
    public class EnumExtensions
    {
        /// <summary>
        /// Get EnumValue by EnumType.
        /// </summary>
        /// <typeparam name="T">EnumType</typeparam>
        /// <returns>List EnumValue entity</returns>
        public static List<EnumValue> GetValues<T>()
        {
            List<EnumValue> values = new List<EnumValue>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(new EnumValue()
                {
                    Name = Enum.GetName(typeof(T), itemType),
                    Value = (int)itemType
                });
            }
            return values;
        }

        /// <summary>
        /// Get all EnumEntity in project.
        /// </summary>
        /// <returns>List EnumEntity</returns>
        public static List<EnumEntity> GetAllValues()
        {
            List<EnumEntity> values = new List<EnumEntity>();

            Type[] vAssembly = Assembly.GetExecutingAssembly().GetTypes();
            List<EnumValue> eValues = new List<EnumValue>();

            foreach (Type enumsInAssembly in vAssembly)
            {
                if (enumsInAssembly.IsEnum)
                {
                    Array enumByReflection = Enum.GetValues(enumsInAssembly);
                    EnumEntity enumEntity = new EnumEntity();
                    enumEntity.Name = enumsInAssembly.Name;
                    eValues = new List<EnumValue>();

                    foreach (var itemType in Enum.GetValues(enumsInAssembly))
                    {
                        //For each value of this enumeration, add a new EnumValue instance
                        eValues.Add(new EnumValue()
                        {
                            Name = Enum.GetName(enumsInAssembly, itemType),
                            Value = (int)itemType
                        });
                    }

                    enumEntity.Value = eValues;
                    values.Add(enumEntity);
                }
            }

            return values;
        }
    }

    /// <summary>
    /// Enum value entity.
    /// </summary>
    public class EnumValue
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    /// <summary>
    /// Enum Entity.
    /// </summary>
    public class EnumEntity
    {
        public string Name { get; set; }
        public List<EnumValue> Value { get; set; }
    }
}