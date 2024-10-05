﻿using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class GraphicsCardService: IGraphicsCardService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public GraphicsCardService(IRepositoryManager repository,
                                   ILoggerManager logger,
                                   IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<GraphicsCardDto> GetAllGraphicsCards(bool trackChanges)
        {
            try
            {
                var graphicsCards = _repository.GraphicsCard.GetAllGraphicsCards(trackChanges);

                var graphicsCardsDto = _mapper.Map<IEnumerable<GraphicsCardDto>>(graphicsCards);

                return graphicsCardsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllGraphicsCards)} service method");
                throw;
            }
        }
    }
}
