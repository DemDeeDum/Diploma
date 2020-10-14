// <copyright file="WebMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using AutoMapper;
    using Hotel.BLL.DTOs;
    using Hotel.BLL.Enums;
    using Hotel.BLL.Models;
    using Hotel.WEB.Models.Admin;
    using Hotel.WEB.Models.Booking;
    using Hotel.WEB.Models.Common;
    using Hotel.WEB.Models.Profile;

    /// <summary>
    /// Maps objects for Web.
    /// </summary>
    public class WebMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebMapper"/> class.
        /// </summary>
        public WebMapper()
        {
            this.CreateMap<string, SelectListItem>()
                .ForMember(
                    dest => dest.Text,
                    opt => opt.MapFrom(src => src))
                .ForMember(
                    dest => dest.Text,
                    opt => opt.MapFrom(src => src));

            this.CreateMap<RoomDTO, RoomViewModel>()
                .ForMember(
                    dest => dest.AppartmentStatus,
                    opt => opt.MapFrom(src => src.AppartmentStatus.ToString()));

            this.CreateMap<RoomViewModel, RoomDTO>()
                .ForMember(
                    dest => dest.AppartmentStatus,
                    opt => opt.MapFrom(src => Enum.Parse(typeof(RoomStatus), src.AppartmentStatus)));

            this.CreateMap<RoomClassDTO, RoomClassViewModel>();

            this.CreateMap<SearchSettings, OrderCreator>();

            this.CreateMap<RoomWholeInfo, RoomWholeInformationViewModel>();

            this.CreateMap<BookingDTO, BookingViewModel>();

            this.CreateMap<UnluckyRequestDTO, UnluckyRequestViewModel>();

            this.CreateMap<ApplicationUserDTO, UserViewModel>();

            this.CreateMap<OperationMessage, LogicMessage>();
        }
    }
}