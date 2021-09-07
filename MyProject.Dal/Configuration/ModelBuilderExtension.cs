using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Dal.Configuration
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder ApplyModelConfiguration(this ModelBuilder builder)
        {

            //builder.ApplyConfiguration(CarMap.Instance);

            //builder.ApplyConfiguration(PersonMap.Instance);


            return builder;
        }
    }
}
