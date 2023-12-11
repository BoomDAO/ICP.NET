//using EdjCase.ICP.Candid.Models;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace EdjCase.ICP.WebSockets
//{
//	public delegate Task<bool> QueueItemCallback<T>(T message);

//	public class QueueArgs<T>
//	{
//		public QueueItemCallback<T> ItemCallback { get; set; }
//		public bool IsDisabled { get; set; } = false;
//	}

//	public class BaseQueue<T>
//	{
//		private readonly Queue<T> _queue = new Queue<T>();
//		private readonly QueueItemCallback<T> _itemCallback;
//		private bool _canProcess = true;
//		private bool _isProcessing = false;

//		public BaseQueue(QueueArgs<T> args)
//		{
//			this._itemCallback = args.ItemCallback ?? throw new ArgumentNullException(nameof(args.ItemCallback));
//			if (args.IsDisabled) this.Disable();
//		}

//		public void Enable() => this._canProcess = true;

//		public void Disable() => this._canProcess = false;

//		public void Add(T item) => this._queue.Enqueue(item);

//		public async Task AddAndProcess(T item)
//		{
//			this.Add(item);
//			await this.Process();
//		}

//		public async Task EnableAndProcess()
//		{
//			this.Enable();
//			await this.Process();
//		}

//		public async Task Process()
//		{
//			if (!this._canProcess || this._isProcessing) return;

//			this._isProcessing = true;
//			await this.ProcessNext();
//		}

//		private async Task ProcessNext()
//		{
//			if (!this._canProcess || this._queue.Count == 0)
//			{
//				this._isProcessing = false;
//				return;
//			}

//			T item = this._queue.Dequeue();
//			bool shouldContinue = await this._itemCallback(item);
//			if (shouldContinue) await this.ProcessNext();
//			else this._isProcessing = false;
//		}
//	}

//	public class AckMessagesQueueArgs
//	{
//		public int ExpirationMs { get; set; }
//		public Action<IEnumerable<BigInteger>> TimeoutExpiredCallback { get; set; }
//	}

//	public class AckMessage
//	{
//		public UnboundedUInt SequenceNumber { get; set; }
//		public long AddedAt { get; set; }
//	}

//	public class AckMessagesQueue
//	{
//		private readonly List<AckMessage> _queue = new List<AckMessage>();
//		private readonly int _expirationMs;
//		private readonly Action<IEnumerable<BigInteger>> _timeoutExpiredCallback;
//		private Timer _lastAckTimeout;

//		public AckMessagesQueue(AckMessagesQueueArgs args)
//		{
//			this._expirationMs = args.ExpirationMs > 0 ? args.ExpirationMs : throw new ArgumentException("ExpirationMs is required");
//			this._timeoutExpiredCallback = args.TimeoutExpiredCallback ?? throw new ArgumentNullException(nameof(args.TimeoutExpiredCallback));
//		}

//		private void StartLastAckTimeout()
//		{
//			this._lastAckTimeout?.Dispose();
//			this._lastAckTimeout = new Timer(OnTimeoutExpired, null, this._expirationMs, Timeout.Infinite);
//		}

//		private void OnTimeoutExpired(object state)
//		{
//			var expiredItems = this._queue.Where(item => DateTimeOffset.Now.ToUnixTimeMilliseconds() - item.AddedAt >= this._expirationMs).Select(item => item.SequenceNumber);
//			this._timeoutExpiredCallback(expiredItems);
//			this._queue.Clear();
//		}

//		public void Add(BigInteger sequenceNumber)
//		{
//			var last = this.Last();
//			if (last != null && sequenceNumber <= last.SequenceNumber)
//				throw new InvalidOperationException($"Sequence number {sequenceNumber} is not greater than last: {last.SequenceNumber}");

//			this._queue.Add(new AckMessage { SequenceNumber = sequenceNumber, AddedAt = DateTimeOffset.Now.ToUnixTimeMilliseconds() });

//			if (this._lastAckTimeout == null) this.StartLastAckTimeout();
//		}

//		public void Ack(BigInteger sequenceNumber)
//		{
//			int index = this._queue.FindIndex(item => item.SequenceNumber == sequenceNumber);
//			if (index >= 0) this._queue.RemoveRange(0, index + 1);
//			else
//			{
//				var last = this.Last();
//				if (last != null && sequenceNumber > last.SequenceNumber)
//					throw new InvalidOperationException($"Sequence number {sequenceNumber} is greater than last: {last.SequenceNumber}");
//			}

//			foreach (var item in this._queue)
//			{
//				if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - item.AddedAt >= this._expirationMs)
//				{
//					this.OnTimeoutExpired(null);
//					return;
//				}
//			}

//			this.StartLastAckTimeout();
//		}

//		public AckMessage Last() => this._queue.LastOrDefault();

//		public void Clear()
//		{
//			this._queue.Clear();
//			this._lastAckTimeout?.Dispose();
//			this._lastAckTimeout = null;
//		}
//	}

//}
