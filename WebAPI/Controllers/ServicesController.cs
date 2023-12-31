﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI.Database;
using WebAPI.DTOs.ServiceDtos;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _serviceRepository.GetAll().ToListAsync();

            List<ServiceGetDto> serviceGetDtos = new List<ServiceGetDto>();
            foreach (var service in services)
            {
                serviceGetDtos.Add(new ServiceGetDto
                {
                    Id = service.Id,
                    Title = service.Title,
                    Description = service.Description,
                    Image = service.Image
                });
            }

            return StatusCode(StatusCodes.Status200OK, serviceGetDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServicePostDto servicePostDto)
        {
            Service service = new()
            {
                Title = servicePostDto.Title,
                Description = servicePostDto.Description,
                Image = servicePostDto.Image,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _serviceRepository.CreateAsync(service);
            await _serviceRepository.SaveAsync();

            return StatusCode(StatusCodes.Status201Created, "Service successfully created");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var services = await _serviceRepository.GetByIdAsync(id);

            if (services is null)
                return NotFound($"Service not found by id: {id}");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ServicePutDto servicePutDto)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service is null)
                return NotFound($"Service not found by id: {id}");

            if (service.Id != servicePutDto.Id)
                return BadRequest();

            service.Title = servicePutDto.Title;
            service.Description = servicePutDto.Description;
            service.Image = servicePutDto.Image;

            _serviceRepository.Update(service);
            await _serviceRepository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service is null)
                return NotFound($"Service not found by id: {id}");

            _serviceRepository.Delete(service);
            await _serviceRepository.SaveAsync();

            return StatusCode(StatusCodes.Status200OK, "Service successfully deleted");
        }
    }
}

