﻿using ApplicationForTrainingWEB.Application.Interfaces;
using ApplicationForTrainingWEB.Application.ViewModels.ReadyPlanWorkoutVm;
using ApplicationForTrainingWEB.Domain.Interfaces;
using ApplicationForTrainingWEB.Domain.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForTrainingWEB.Application.Services
{
    public class ReadyPlanWorkoutService : IReadyPlanWorkoutService
    {
        private readonly IReadyPlanWorkoutRepository _readyPlanWorkoutRepository;
        private readonly IMapper _mapper;


        public ReadyPlanWorkoutService(IReadyPlanWorkoutRepository readyPlanWorkoutRepository, IMapper mapper)
        {
            _readyPlanWorkoutRepository = readyPlanWorkoutRepository;
            _mapper = mapper;

        }
        public int AddReadyPlanWorkout(NewReadyPlanWorkoutVm model)
        {
            var newPlan = new ReadyPlanWorkout
            {
                Name = model.Name,
                PlanType = model.PlanType,
                Difficulty = model.Difficulty,
                Description = model.Description,
                PlanDetails = model.PlanDetails,
                UserId = model.UserId
            };

            _readyPlanWorkoutRepository.AddReadyPlanWorkout(newPlan);
            return newPlan.Id;
        }

        public void DeleteReadyPlanWorkout(int id)
        {
            var readyPlanWorkout = _readyPlanWorkoutRepository.GetDetail(id);

            if (readyPlanWorkout != null)
            {
                _readyPlanWorkoutRepository.DeleteReadyPlanWorkout(readyPlanWorkout);
            }
        }

        public ListReadyPlanWorkoutVm GetAllReadyPlanWorkoutsForList(int pageSize, int pageNo, string searchString)
        {
            var query = _readyPlanWorkoutRepository.GetAllPlanReadyWorkout();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.Name.Contains(searchString) || p.PlanType.Contains(searchString));
            }

            var count = query.Count();
            var plans = query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(plan => new ReadyPlanWorkoutForListVm
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    UserId = plan.UserId,
                    Description = plan.Description,
                    PlanDetails = plan.PlanDetails
                }).ToList();

            return new ListReadyPlanWorkoutVm
            {
                ReadyPlanWorkouts = plans,
                CurrentPage = pageNo,
                PageSize = pageSize,
                SearchString = searchString,
                Count = count
            };
        }

        public IQueryable<ReadyPlanWorkout> GetPlansByDifficulty(string difficulty)
        {
            return _readyPlanWorkoutRepository.GetPlansByDifficulty(difficulty);
        }

        public IQueryable<ReadyPlanWorkout> GetPlansByType(string type)
        {
            return _readyPlanWorkoutRepository.GetPlansByType(type);
        }

        public IQueryable<ReadyPlanWorkout> GetPlansByTypeAndDifficulty(string type, string difficulty)
        {
            return _readyPlanWorkoutRepository.GetPlansByTypeAndDifficulty(type, difficulty);
        }
        public List<ReadyPlanWorkoutForListVm> GetPlansByTypeAndDifficulty2(string type, string difficulty)
        {
            var plans = _readyPlanWorkoutRepository.GetAllPlanReadyWorkout()
                            .Where(p => p.PlanType == type && p.Difficulty == difficulty)
                            .Select(p => new ReadyPlanWorkoutForListVm
                            {
                                Name = p.Name,
                                PlanType = p.PlanType,
                                Difficulty = p.Difficulty,
                                Description = p.Description,
                                PlanDetails = p.PlanDetails
                            })
                            .ToList();

            return plans;
        }
    }
}
