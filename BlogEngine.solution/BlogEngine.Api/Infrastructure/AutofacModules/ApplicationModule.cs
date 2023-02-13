using Autofac;
using BlogEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Application;
using BlogEngine.Infrastructure.Repositories;
using BlogEngine.Domain.AggregatesModel;
using BlogEngine.Application.Queries;

namespace BlogEngine.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<PostQueries>()
                    .As<IPostQueries>()
                    .InstancePerLifetimeScope();

            builder.RegisterType<CommentQueries>()
                    .As<ICommentQueries>()
                    .InstancePerLifetimeScope();

            builder.RegisterType<SecurityQueries>()
                    .As<ISecurityQueries>()
                    .InstancePerLifetimeScope();

            builder.RegisterType<PostRepository>()
                    .As<IPostRepository>()
                    .InstancePerLifetimeScope();

            builder.RegisterType<CommentRepository>()
                    .As<ICommentRepository>()
                    .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().InstancePerLifetimeScope();
        }
    }
}
