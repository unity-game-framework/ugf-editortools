using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.EditorTools.Editor.Asset.Processor
{
    internal class AssetProcessorProvider
    {
        public int Count { get { return m_processors.Count; } }

        private readonly Dictionary<string, ProcessorCollection> m_processors = new Dictionary<string, ProcessorCollection>();
        private readonly Dictionary<string, OrderCollection> m_orders = new Dictionary<string, OrderCollection>();
        private readonly IComparer<IAssetProcessor> m_comparer;

        public struct Enumerator : IEnumerator<KeyValuePair<string, ProcessorCollectionEnumerable>>
        {
            public KeyValuePair<string, ProcessorCollectionEnumerable> Current { get; set; }

            object IEnumerator.Current { get { return Current; } }

            private Dictionary<string, ProcessorCollection>.Enumerator m_enumerator;

            internal Enumerator(Dictionary<string, ProcessorCollection>.Enumerator enumerator)
            {
                m_enumerator = enumerator;
            }

            public bool MoveNext()
            {
                if (m_enumerator.MoveNext())
                {
                    KeyValuePair<string, ProcessorCollection> current = m_enumerator.Current;
                    var enumerable = new ProcessorCollectionEnumerable(current.Value);

                    Current = new KeyValuePair<string, ProcessorCollectionEnumerable>(current.Key, enumerable);

                    return true;
                }

                return false;
            }

            void IEnumerator.Reset()
            {
            }

            void IDisposable.Dispose()
            {
            }
        }

        public struct ProcessorCollectionEnumerable
        {
            private readonly Dictionary<Type, IAssetProcessor> m_collection;

            internal ProcessorCollectionEnumerable(Dictionary<Type, IAssetProcessor> collection)
            {
                m_collection = collection;
            }

            public Dictionary<Type, IAssetProcessor>.Enumerator GetEnumerator()
            {
                return m_collection.GetEnumerator();
            }
        }

        internal class ProcessorCollection : Dictionary<Type, IAssetProcessor>
        {
            public ReadOnlyDictionary<Type, IAssetProcessor> AsReadOnly { get; }

            public ProcessorCollection()
            {
                AsReadOnly = new ReadOnlyDictionary<Type, IAssetProcessor>(this);
            }
        }

        private class OrderCollection : List<IAssetProcessor>
        {
            public new ReadOnlyCollection<IAssetProcessor> AsReadOnly { get; }

            public OrderCollection()
            {
                AsReadOnly = new ReadOnlyCollection<IAssetProcessor>(this);
            }

            public void Remove(Type processorType)
            {
                for (int i = 0; i < Count; i++)
                {
                    IAssetProcessor processor = this[i];

                    if (processor.GetType() == processorType)
                    {
                        RemoveAt(i);
                    }
                }
            }
        }

        public AssetProcessorProvider(IComparer<IAssetProcessor> comparer = null)
        {
            m_comparer = comparer ?? AssetProcessorComparer.Default;
        }

        public void Add(string guid, IAssetProcessor processor)
        {
            if (string.IsNullOrEmpty(guid)) throw new ArgumentException("Value cannot be null or empty.", nameof(guid));
            if (processor == null) throw new ArgumentNullException(nameof(processor));

            if (!m_processors.TryGetValue(guid, out ProcessorCollection collection))
            {
                collection = new ProcessorCollection();

                m_processors.Add(guid, collection);
            }

            if (!m_orders.TryGetValue(guid, out OrderCollection orders))
            {
                orders = new OrderCollection();

                m_orders.Add(guid, orders);
            }

            Type type = processor.GetType();

            collection.Add(type, processor);
            orders.Add(processor);
            orders.Sort(m_comparer);
        }

        public void Remove(string guid)
        {
            if (string.IsNullOrEmpty(guid)) throw new ArgumentException("Value cannot be null or empty.", nameof(guid));

            m_processors.Remove(guid);
            m_orders.Remove(guid);
        }

        public void Remove(string guid, Type processorType)
        {
            if (string.IsNullOrEmpty(guid)) throw new ArgumentException("Value cannot be null or empty.", nameof(guid));
            if (processorType == null) throw new ArgumentNullException(nameof(processorType));

            if (m_processors.TryGetValue(guid, out ProcessorCollection collection))
            {
                if (collection.Remove(processorType) && collection.Count == 0)
                {
                    m_processors.Remove(guid);
                    m_orders.Remove(guid);
                }

                if (m_orders.TryGetValue(guid, out OrderCollection orders))
                {
                    orders.Remove(processorType);
                }
            }
        }

        public void Clear()
        {
            m_processors.Clear();
            m_orders.Clear();
        }

        public bool TryGet<T>(string guid, Type processorType, out T processor) where T : IAssetProcessor
        {
            if (TryGet(guid, processorType, out IAssetProcessor value) && value is T cast)
            {
                processor = cast;
                return true;
            }

            processor = default;
            return false;
        }

        public bool TryGet(string guid, Type processorType, out IAssetProcessor processor)
        {
            if (string.IsNullOrEmpty(guid)) throw new ArgumentException("Value cannot be null or empty.", nameof(guid));
            if (processorType == null) throw new ArgumentNullException(nameof(processorType));

            if (m_processors.TryGetValue(guid, out ProcessorCollection collection) && collection.TryGetValue(processorType, out processor))
            {
                return true;
            }

            processor = null;
            return false;
        }

        public bool TryGetAll(string guid, out IReadOnlyDictionary<Type, IAssetProcessor> processors)
        {
            if (string.IsNullOrEmpty(guid)) throw new ArgumentException("Value cannot be null or empty.", nameof(guid));

            if (m_processors.TryGetValue(guid, out ProcessorCollection collection))
            {
                processors = collection.AsReadOnly;
                return true;
            }

            processors = null;
            return false;
        }

        public bool TryGetAllOrdered(string guid, out IReadOnlyList<IAssetProcessor> processors)
        {
            if (string.IsNullOrEmpty(guid)) throw new ArgumentException("Value cannot be null or empty.", nameof(guid));

            if (m_orders.TryGetValue(guid, out OrderCollection collection))
            {
                processors = collection.AsReadOnly;
                return true;
            }

            processors = null;
            return false;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(m_processors.GetEnumerator());
        }
    }
}
