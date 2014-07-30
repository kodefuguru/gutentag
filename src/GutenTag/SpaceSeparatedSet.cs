namespace Guten
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class SpaceSeparatedSet : ISet<string>
    {
        private readonly HashSet<string> hash;

        public SpaceSeparatedSet()
        {
            hash = new HashSet<string>();
        }

        private IEnumerable<string> ParseNames(IEnumerable<string> names)
        {
            return names.SelectMany(s => s.Split(new[] { ' ' }));
        }

        private IEnumerable<string> ParseNames(string names)
        {
            return ParseNames(new[] { names });
        }

        public SpaceSeparatedSet(string classes)
            : this()
        {
            this.Add(classes);
        }

        public SpaceSeparatedSet(IEnumerable<string> classes)
            : this()
        {
            foreach (var c in classes)
            {
                this.Add(c);
            }
        }

        void ICollection<string>.Add(string item)
        {
            this.Add(item);
        }

        public void Clear()
        {
            hash.Clear();
        }

        public bool Contains(string cssClass)
        {
            return hash.Contains(cssClass);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            hash.CopyTo(array, arrayIndex);
        }

        private bool ParsedRemove(IEnumerable<string> names)
        {
            bool result = names.Any();
            foreach (var name in names)
            {
                result |= hash.Remove(name);
            }
            return result;
        }

        /// <returns>true if the set changed</returns>
        public bool Remove(IEnumerable<string> names)
        {
            return ParsedRemove(ParseNames(names));
        }

        /// <returns>true if the set changed</returns>
        public bool Remove(string names)
        {
            return ParsedRemove(ParseNames(names));
        }

        public int Count
        {
            get { return hash.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return hash.GetEnumerator();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return hash.GetEnumerator();
        }

        private bool ParsedAdd(IEnumerable<string> names)
        {
            bool result = names.Any();
            foreach (var name in names)
            {
                result |= hash.Add(name);
            }
            return result;
        }

        /// <returns>true if the set changed</returns>
        public bool Add(IEnumerable<string> names)
        {
            return ParsedAdd(ParseNames(names));
        }

        /// <returns>true if the set changed</returns>
        public bool Add(string names)
        {
            return ParsedAdd(ParseNames(names));
        }

        public void UnionWith(IEnumerable<string> other)
        {
            hash.UnionWith(new SpaceSeparatedSet(other));
        }

        public void IntersectWith(IEnumerable<string> other)
        {
            hash.IntersectWith(new SpaceSeparatedSet(other));
        }

        public void ExceptWith(IEnumerable<string> other)
        {
            hash.ExceptWith(new SpaceSeparatedSet(other));
        }

        public void SymmetricExceptWith(IEnumerable<string> other)
        {
            hash.SymmetricExceptWith(new SpaceSeparatedSet(other));
        }

        public bool IsSubsetOf(IEnumerable<string> other)
        {
            return hash.IsSubsetOf(new SpaceSeparatedSet(other));
        }

        public bool IsSupersetOf(IEnumerable<string> other)
        {
            return hash.IsSupersetOf(new SpaceSeparatedSet(other));
        }

        public bool IsProperSupersetOf(IEnumerable<string> other)
        {
            return hash.IsProperSupersetOf(new SpaceSeparatedSet(other));
        }

        public bool IsProperSubsetOf(IEnumerable<string> other)
        {
            return hash.IsProperSubsetOf(new SpaceSeparatedSet(other));
        }

        public bool Overlaps(IEnumerable<string> other)
        {
            return hash.Overlaps(new SpaceSeparatedSet(other));
        }

        public bool SetEquals(IEnumerable<string> other)
        {
            return hash.SetEquals(new SpaceSeparatedSet(other));
        }

        public override string ToString()
        {
            switch (Count)
            {
                case 0: return String.Empty;
                case 1: return hash.First();
                default: return hash.Aggregate((a, b) => a + " " + b);
            }
        }
    }
}