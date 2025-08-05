import { Ferias } from "./ferias.model";
import { HistoricoAlteracao } from "./historico.model";

export interface Funcionario {
  id: number;
  nome: string;
  cargo: string;
  dataAdmissao: Date;
  salario: number;
  status: boolean;
  ferias?: Ferias[];
  historicoAlteracoes?: HistoricoAlteracao[];
}