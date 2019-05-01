using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ScientificReport.Test
{
	public class MockProvider
	{
		public static Mock<DbSet<T>> GetMockSet<T>(IEnumerable<T> testData) where T : class
		{
			var list = testData.AsQueryable();
			var mockSet = new Mock<DbSet<T>>();
			mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(list.Provider);
			mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(list.Expression);
			mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(list.ElementType);
			mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
			return mockSet;
		}
	}
}
