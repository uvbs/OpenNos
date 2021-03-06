﻿/*
 * This file is part of the OpenNos Emulator Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 */

using AutoMapper;

using OpenNos.DAL.EF.MySQL.Helpers;
using OpenNos.DAL.Interface;
using OpenNos.Data;
using System.Collections.Generic;
using System.Linq;

namespace OpenNos.DAL.EF.MySQL
{
    public class SkillDAO : ISkillDAO
    {
        #region Members

        private IMapper _mapper;

        #endregion

        #region Instantiation

        public SkillDAO()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Skill, SkillDTO>();
                cfg.CreateMap<SkillDTO, Skill>();
            });

            _mapper = config.CreateMapper();
        }

        #endregion

        #region Methods

        public void Insert(List<SkillDTO> skills)
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                foreach (SkillDTO Skill in skills)
                {
                    Skill entity = _mapper.Map<Skill>(Skill);
                    context.Skill.Add(entity);
                }
                context.Configuration.AutoDetectChangesEnabled = true;
                context.SaveChanges();
            }
        }

        public SkillDTO Insert(SkillDTO skill)
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                Skill entity = _mapper.Map<Skill>(skill);
                context.Skill.Add(entity);
                context.SaveChanges();
                return _mapper.Map<SkillDTO>(entity);
            }
        }

        public IEnumerable<SkillDTO> LoadAll()
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                foreach (Skill Skill in context.Skill)
                {
                    yield return _mapper.Map<SkillDTO>(Skill);
                }
            }
        }

        public SkillDTO LoadById(short skillId)
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                return _mapper.Map<SkillDTO>(context.Skill.FirstOrDefault(s => s.SkillVNum.Equals(skillId)));
            }
        }

        #endregion
    }
}