using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Identity.Client;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EfUserDao>().As<IUserDao>();
        builder.RegisterType<UserManager>().As<IUserService>();

        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        builder.RegisterType<EfPostDao>().As<IPostDao>();
        builder.RegisterType<PostManager>().As<IPostService>();
        
        builder.RegisterType<EfCommentDao>().As<ICommentDao>();
        builder.RegisterType<CommentManager>().As<ICommentService>();
        
        builder.RegisterType<EfLikePostDao>().As<ILikePostDao>();
        builder.RegisterType<EfLikeCommentDao>().As<ILikeCommentDao>();
        builder.RegisterType<LikeManager>().As<ILikeService>();
        
        builder.RegisterType<EfCommentOfPostDao>().As<ICommentOfPostDao>();
        
        builder.RegisterType<EfFriendRequestDao>().As<IFriendRequestDao>();
        builder.RegisterType<FriendRequestManager>().As<IFriendRequestService>();
        
        builder.RegisterType<EfFriendDao>().As<IFriendDao>();
        builder.RegisterType<FriendManager>().As<IFriendService>();
        
        builder.RegisterType<EfPhotoDao>().As<IPhotoDao>();
        builder.RegisterType<PhotoManager>().As<IPhotoService>();
    }
}