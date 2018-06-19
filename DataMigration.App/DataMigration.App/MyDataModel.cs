using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DataMigration.App
{
    public static class MyDataModel
    {
        public static DbContextOptionsBuilder UseMyModel(this DbContextOptionsBuilder dbContextBuilder)
        {

            CoreConventionSetBuilderDependencies ds = new CoreConventionSetBuilderDependencies(new TypeMapper(), null, null, null, null, null);
            //CoreConventionSetBuilderDependencies ds = new CoreConventionSetBuilderDependencies(new TypeMapper());

            var builder = new ModelBuilder(new CoreConventionSetBuilder(ds).CreateConventionSet());

            var mappings = RegisterMappers();

            foreach (var entityMapper in mappings)
            {
                entityMapper.MapEntity(builder);
            }

            dbContextBuilder.UseModel(builder.Model);

            return dbContextBuilder;
        }

        private static IList<IEntityMapper> RegisterMappers()
        {
            return new List<IEntityMapper>()
            {
                new BlogMapping(),
                new PostMapping()
            };
        }
    }

    public class CoreTypeMappingDerive : CoreTypeMapping
    {
        public CoreTypeMappingDerive() : base(new CoreTypeMappingParameters())
        {

        }
        public override CoreTypeMapping Clone(ValueConverter converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            throw new NotImplementedException();
        }
    }
    public class TypeMapper : ITypeMappingSource //ITypeMapper 
    {
        CoreTypeMappingDerive _coreTypeMappingDerive = null;
        public TypeMapper()
        {
            //Error will occurred if this below line will uncomment; Need to more investigation, how to implement the FindMapping methods 
            //_coreTypeMappingDerive = new CoreTypeMappingDerive();
        }

        public CoreTypeMapping FindMapping(IProperty property)
        {
            return _coreTypeMappingDerive;
        }

        public CoreTypeMapping FindMapping(MemberInfo member)
        {
            return _coreTypeMappingDerive;
        }

        public CoreTypeMapping FindMapping(Type type)
        {
            return _coreTypeMappingDerive;
        }
    }
}
