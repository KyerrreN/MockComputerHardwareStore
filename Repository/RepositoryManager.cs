﻿using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        // fields to work with
        private readonly RepositoryContext _context;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IGraphicsCardRepository> _graphicsCardRepository;

        // DI
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(context));
            _graphicsCardRepository = new Lazy<IGraphicsCardRepository>(() => new GraphicsCardRepository(context));
        }

        // Properties to serve needed repositories
        public ICategoryRepository Category => _categoryRepository.Value;
        public IGraphicsCardRepository GraphicsCard => _graphicsCardRepository.Value;

        // Method to save changes
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}