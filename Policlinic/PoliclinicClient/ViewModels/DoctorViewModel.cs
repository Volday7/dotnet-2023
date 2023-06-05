﻿using ReactiveUI;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive;

namespace PoliclinicClient.ViewModels;
public class DoctorViewModel : ViewModelBase
{
    private int _id;
    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    private string _fio = string.Empty;
    [Required]
    public string Fio
    {
        get => _fio;
        set => this.RaiseAndSetIfChanged(ref _fio, value);
    }

    private DateTimeOffset _birthDate = DateTime.Now;
    [Required]
    public DateTimeOffset BirthDate
    {
        get => _birthDate;
        set => this.RaiseAndSetIfChanged(ref _birthDate, value);
    }

    private long _passport;
    [Required]
    public long Passport
    {
        get => _passport;
        set => this.RaiseAndSetIfChanged(ref _passport, value);
    }

    private int _workExperience;
    [Required]
    public int WorkExperience
    {
        get => _workExperience;
        set => this.RaiseAndSetIfChanged(ref _workExperience, value);
    }

    private int _specializationId;
    [Required]
    public int SpecializationId
    {
        get => _specializationId;
        set => this.RaiseAndSetIfChanged(ref _specializationId, value);
    }

    public ReactiveCommand<Unit, DoctorViewModel> OnSubmitCommand { get; }

    public DoctorViewModel()
    {
        OnSubmitCommand = ReactiveCommand.Create(() => this);
    }
}
