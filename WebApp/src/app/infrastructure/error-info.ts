

export interface ErrorDetails {
    Dtatus: number;
    Message: string;
    ValidationErrors: RuleViolation[];

}
export interface RuleViolation {
    Identifier: string;
    Message: string;
    IsWarning: boolean;
}
