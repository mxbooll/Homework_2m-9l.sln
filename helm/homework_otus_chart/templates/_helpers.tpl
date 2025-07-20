{{/* vim: set filetype=mustache: */}}
{{/*
Expand the name of the chart.
*/}}
{{- define "homework-otus-chart.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/}}
{{- define "homework-otus-chart.fullname" -}}
{{- if .Values.fullnameOverride -}}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- $name := default .Chart.Name .Values.nameOverride -}}
{{- if contains $name .Release.Name -}}
{{- .Release.Name | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" -}}
{{- end -}}
{{- end -}}
{{- end -}}

{{/*
Create chart name and version as used by the chart label.
*/}}
{{- define "homework-otus-chart.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Common labels
*/}}
{{- define "homework-otus-chart.labels" -}}
helm.sh/chart: {{ include "homework-otus-chart.chart" . }}
{{ include "homework-otus-chart.selectorLabels" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end -}}

{{/*
Selector labels
*/}}
{{- define "homework-otus-chart.selectorLabels" -}}
app.kubernetes.io/name: {{ include "homework-otus-chart.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end -}}

{{/*
Create the name of the service account to use
*/}}
{{- define "homework-otus-chart.serviceAccountName" -}}
{{- if .Values.serviceAccount.create -}}
{{- default (include "homework-otus-chart.fullname" .) .Values.serviceAccount.name -}}
{{- else -}}
{{- default "default" .Values.serviceAccount.name -}}
{{- end -}}
{{- end -}}

{{/*
Postgres connection string template
*/}}
{{- define "homework-otus-chart.postgresConnectionString" -}}
{{- $secret := (lookup "v1" "Secret" .Release.Namespace (printf "%s-postgres" (include "homework-otus-chart.fullname" .))) -}}
{{- if $secret -}}
Server={{ $secret.data.postgresHost | b64dec }}:{{ $secret.data.postgresPort | b64dec }};
Database={{ $secret.data.postgresDb | b64dec }};
User Id={{ $secret.data.postgresUser | b64dec }};
Password={{ $secret.data.postgresPassword | b64dec }};
{{- end -}}
{{- end -}}