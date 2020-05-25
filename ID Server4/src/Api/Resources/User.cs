using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Resources
{
    //[ObsoleteAttribute]
    public class User
    {
        /// <summary>
        /// The Id of the user, it is Integer based
        /// </summary>
        /// <example>5</example>
        public int UserId { get; set; }
        
        /// <summary>
        /// The first name of the person
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Birth date of the user
        /// </summary>
        /// <example>1/23/2001</example>
        public DateTime DateOfBirth { get; set; }
    }
}
