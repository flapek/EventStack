namespace EventStack_API.Helpers
{
    abstract class AbstractFactory
    {
        public abstract void insertOne();
        public abstract void insertMany();
        public abstract void find();
        public abstract void updateOne();
        public abstract void updateMany();
        public abstract void deleteOne();
        public abstract void deleteMany();
    }
}