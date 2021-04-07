using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectExportDto
    {
        [XmlElement("ProjectName")]
        public string Name { get; set; }

        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public TaskExportDto[] Tasks { get; set; }
    }

    [XmlType("Task")]
    public class TaskExportDto
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public string Label { get; set; }
    }
}
