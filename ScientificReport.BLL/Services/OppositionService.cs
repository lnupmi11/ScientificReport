using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.Opposition;

namespace ScientificReport.BLL.Services
{
	public class OppositionService : IOppositionService
	{
		private readonly OppositionRepository _oppositionRepository;

		public OppositionService(ScientificReportDbContext context)
		{
			_oppositionRepository = new OppositionRepository(context);
		}

		public virtual IEnumerable<Opposition> GetAll()
		{
			return _oppositionRepository.All();
		}

		public virtual IEnumerable<Opposition> GetAllWhere(Func<Opposition, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<Opposition> GetPage(int page, int count)
		{
			return _oppositionRepository.All().Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCount()
		{
			return _oppositionRepository.All().Count();
		}

		public virtual Opposition GetById(Guid id)
		{
			return _oppositionRepository.Get(id);
		}

		public virtual Opposition Get(Func<Opposition, bool> predicate)
		{
			return _oppositionRepository.Get(predicate);
		}

		public virtual void CreateItem(OppositionModel model)
		{
			_oppositionRepository.Create(new Opposition
			{
				About = model.About,
				Opponent = model.Opponent,
				DateOfOpposition = model.DateOfOpposition
			});
		}

		public virtual void UpdateItem(OppositionEditModel model)
		{
			var opposition = GetById(model.Id);
			if (opposition == null)
			{
				return;
			}
			
			opposition.About = model.About;
			opposition.Opponent = model.Opponent;
			opposition.DateOfOpposition = model.DateOfOpposition;
			_oppositionRepository.Update(opposition);
		}

		public virtual void DeleteById(Guid id)
		{
			_oppositionRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _oppositionRepository.Get(id) != null;
		}
	}
}
