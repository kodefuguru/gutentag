namespace Guten
{
    public abstract class TagWriterFactory
    {
        public abstract TagWriter CreateWriter(Tag tag);
    }
}