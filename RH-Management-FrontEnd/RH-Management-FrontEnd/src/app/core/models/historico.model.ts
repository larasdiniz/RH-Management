export interface HistoricoAlteracao {
  id: number;
  funcionarioId: number;
  dataAlteracao: Date;
  campoAlterado: string;
  valorAntigo?: string;
  valorNovo?: string;
}
