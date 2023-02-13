namespace BlogEngine.Common
{
    public enum PostStatusEnum
    {
        PENDING = 1,
        PENDING_APROVAL = 2,
        PUBLISHED = 3,
        REJECTED = 4
    }

    public enum RolesEnum
    {
        PUBLIC = 1,
        WRITER = 2,
        EDITOR = 3
    }

    public enum CommentType
    {
        GENERAL= 1,
        REJECTED = 2,
        NONE = 3
    }
}
