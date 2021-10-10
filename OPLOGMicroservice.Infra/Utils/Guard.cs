using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Infra.Utils
{
    public static class Guard
    {
        /// <summary>
        /// Checks for null or default objects and throws NullReferenceException on first occurrence.
        /// </summary>
        /// <param name="objects">
        /// Array of object to check and parameter name to add into the exception message
        /// string section of this parameter will be added into exception message like (myObject, objName): "objName cannot be null or empty !"
        /// </param>
        /// <returns>True if validation errors occur.</returns>
        public static bool NullOrDefault(params object[] objects)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                // Null is not acceptable in any way
                if (objects[i] == null)
                {
                    return true;
                }

                // If object is a guid, Default value is not acceptable
                if (objects[i] is Guid && Guid.Parse(objects[i].ToString()) == default)
                {
                    return true;
                }

                // If object is a string, we ask for if empty strings are allowed
                if (objects[i] is string && string.IsNullOrEmpty(objects[i].ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks for null or default objects and throws NullReferenceException on first occurrence.
        /// </summary>
        /// <param name="failedValidations">Adds failed validationItem to the list</param>
        /// <param name="objects">
        /// Array of object to check and parameter name to add into the exception message
        /// string section of this parameter will be added into exception message like (myObject, objName): "objName cannot be null or empty !"
        /// </param>
        /// <returns>True if validation errors occur.</returns>
        public static bool NullOrDefault(List<string> failedValidations, params object[] objects)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                // Null is not acceptable in any way
                if (objects[i] == null)
                {
                    failedValidations.Add($"{i}. parameter is null");
                }

                // If object is a guid, Default value is not acceptable
                if (objects[i] is Guid && Guid.Parse(objects[i].ToString()) == default)
                {
                    failedValidations.Add($"{i}. guid parameter is default");
                }

                // If object is a string, we ask for if empty strings are allowed
                if (objects[i] is string && string.IsNullOrEmpty(objects[i].ToString()))
                {
                    failedValidations.Add($"{i}. string parameter is null or empty");
                }
            }

            return failedValidations.Count > 0;
        }

        public static bool NullOrEmpty<T>(ICollection<T> collection)
        {
            return collection == null || !collection.Any();
        }
    }
}
