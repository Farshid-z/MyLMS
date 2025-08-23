using Abp.Zero.EntityFrameworkCore;
using MyLMS.Authorization.Roles;
using MyLMS.Authorization.Users;
using MyLMS.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using MyLMS.Entities;

namespace MyLMS.EntityFrameworkCore;

public class MyLMSDbContext : AbpZeroDbContext<Tenant, Role, User, MyLMSDbContext>
{
    /* Define a DbSet for each entity of the application */
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Roadmap> Roadmaps { get; set; }
    public DbSet<Session> Sessions  { get; set; }
    public DbSet<Student> Students  { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<StudentLesson> StudentLessons { get; set; }
    public DbSet<StudentRoadmap> StudentRoadmaps { get; set; }
    public DbSet<StudentSession> StudentSessions { get; set; }
    public MyLMSDbContext(DbContextOptions<MyLMSDbContext> options)
        : base(options)
    {
    }
}
