namespace MyLMS.Enums
{
    public enum DeleteRoadmapResult
    { 
        Success,
        RecordNotFound,
        RoadmapHasCours
    }

    public enum DeleteCourseResult
    {
        Success,
        RecordNotFound,
        CourseHasSession
    }

    public enum DeleteSessionResult
    {
        Success,
        RecordNotFound,
        SessionHasLession
    }
    public enum DeleteTeacherResult 
    {
        Success,
        RecordNotFound,
        TeacherHasCourse
    }

    public enum BasicDeleteResult
    {
        Success,
        RecordNotFound 
    }

    public enum AsignRoadmapToStudentResult
    {
        Success,
        StudentNotFound,
        RoadmapNotFound,
        RoadmapAlreadyAsigned,
        FirstCourseNotFound,
        FirstSessionNotFound,
        FirstLessonNotFound,
        FailedToAsigneRoadmap,
        FailedToAsignCourse,
        FailedToAsignSession,
        FailedToAsignLesson
    }

    public enum StudentLessonActionsResult
    {
        Success,
        StudentNotFound,
        LessonNotFound,
        LessonAlreadyFinished,
        NotStudentLesson,
        StudentSessionNotFound,
        StudentCourseNotFound,
        StudentRoadmapNotFound
    }
}
