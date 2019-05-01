using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class OppositionService : IOppositionService
	{
		private readonly OppositionRepository _oppositionRepository;

		public OppositionService(ScientificReportDbContext context)
		{
			_oppositionRepository = new OppositionRepository(context);
		}

		public IEnumerable<Opposition> GetAll()
		{
			return _oppositionRepository.All();
		}

		public IEnumerable<Opposition> GetAllWhere(Func<Opposition, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public Opposition GetById(Guid id)
		{
			return _oppositionRepository.Get(id);
		}

		public Opposition Get(Func<Opposition, bool> predicate)
		{
			return _oppositionRepository.Get(predicate);
		}

		public void CreateItem(Opposition opposition)
		{
			_oppositionRepository.Create(opposition);
		}

		public void UpdateItem(Opposition opposition)
		{
			_oppositionRepository.Update(opposition);
		}

		public void DeleteById(Guid id)
		{
			_oppositionRepository.Delete(id);
		}

		public bool Exists(Guid id)
		{
			return _oppositionRepository.Get(id) != null;
		}
	}
}
