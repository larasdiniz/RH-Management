import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { Funcionario } from "../../core/models/funcionario.model";
import { Component, OnInit } from "@angular/core";
import { FuncionarioService } from "../../core/services/funcionario.service";
import { ActivatedRoute, Router, RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-funcionario-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  template: `<form [formGroup]="form" (ngSubmit)="salvar()">
  <label>Nome: <input formControlName="nome"></label>
  <label>Cargo: <input formControlName="cargo"></label>
  <label>Data Admissão: <input type="date" formControlName="dataAdmissao"></label>
  <label>Salário: <input type="number" formControlName="salario"></label>
  <label>Status: <input type="checkbox" formControlName="status"></label>
  <button type="submit">Salvar</button>
</form>`
})
export class FuncionarioFormComponent implements OnInit {
  form!: FormGroup;
  funcionarioId: number | null = null;

  constructor(private fb: FormBuilder, private service: FuncionarioService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    this.form = this.fb.group({
      nome: ['', Validators.required],
      cargo: ['', Validators.required],
      dataAdmissao: ['', Validators.required],
      salario: [0, Validators.required],
      status: [true]
    });

    this.funcionarioId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.funcionarioId) {
      this.service.getById(this.funcionarioId).subscribe(f => this.form.patchValue(f));
    }
  }

  salvar() {
    const funcionario: Funcionario = this.form.value;
    if (this.funcionarioId) {
      funcionario.id = this.funcionarioId;
      this.service.update(funcionario).subscribe(() => this.router.navigate(['/home']));
    } else {
      this.service.create(funcionario).subscribe(() => this.router.navigate(['/home']));
    }
  }
}